import {
  Alert,
  Chip,
  Box,
  Button,
  CircularProgress,
  Stack,
  TextField,
  Typography,
  Snackbar,
} from "@mui/material";
import dayjs from "dayjs";
import "dayjs/locale/ru";
import { skipToken } from "@reduxjs/toolkit/query";
import { useParams } from "react-router";
import { useEffect, useState } from "react";
import {
  useGetAuthoringQuery,
  useSubmitAuthoringMutation,
  useUpdateAuthoringMutation,
} from "./api";
import { getStatusColor, getStatusLabel } from "./helpers/helpers";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

const schema = z.object({
  comment: z.string().max(1000, "Комментарий слишком длинный").optional(),
  firstName: z.string().optional(),
  lastName: z.string().optional(),
  middleName: z.string().optional(),
});

type FormFields = z.infer<typeof schema>;

export default function MyAuthoringPage() {
  const { id } = useParams<{ id: string }>();

  const { data, isLoading, isError } = useGetAuthoringQuery(
    id ? { id } : skipToken
  );

  const [submitAuthoring, { isLoading: isSubmitting }] =
    useSubmitAuthoringMutation();
  const [updateAuthoring] = useUpdateAuthoringMutation();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting: isSaving },
  } = useForm<FormFields>({
    resolver: zodResolver(schema),
  });

  const [snackbar, setSnackbar] = useState<{
    open: boolean;
    message: string;
    severity: "success" | "error";
  }>({ open: false, message: "", severity: "success" });

  useEffect(() => {
    if (data?.result) {
      reset({
        comment: data.result.comment ?? "",
        firstName: data.result.firstName ?? "",
        lastName: data.result.lastName ?? "",
        middleName: data.result.middleName ?? "",
      });
    }
  }, [data, reset]);

  const status = data?.result?.status;
  const isDraft = status === "Draft";
  const isRejected = status === "Rejected";
  const isAccepted = status === "Accepted";

  async function handleSend() {
    if (!id) return;
    try {
      await submitAuthoring({ authoringId: id }).unwrap();
      setSnackbar({
        open: true,
        message: "Заявка отправлена на рассмотрение",
        severity: "success",
      });
    } catch {
      setSnackbar({
        open: true,
        message: "Ошибка при отправке заявки",
        severity: "error",
      });
    }
  }

  async function onSubmit(formData: FormFields) {
    if (!id) return;
    try {
      await updateAuthoring({ id, ...formData }).unwrap();
      setSnackbar({
        open: true,
        message: "Заявка успешно сохранена",
        severity: "success",
      });
    } catch {
      setSnackbar({
        open: true,
        message: "Ошибка при сохранении",
        severity: "error",
      });
    }
  }

  if (isLoading) {
    return (
      <Box className="flex justify-center mt-10">
        <CircularProgress />
      </Box>
    );
  }

  if (isError || !data?.result) {
    return (
      <Alert severity="error" className="my-10">
        Не удалось загрузить заявку.
      </Alert>
    );
  }

  return (
    <Box className="p-6 space-y-6">
      <Typography variant="h4">Заявка на роль автора</Typography>

      <div className="flex gap-3 items-start flex-col">
        <Typography>
          Дата создания:{" "}
          <strong>
            {dayjs(data.result.createdAt).format("D MMMM YYYY [в] HH:mm")}
          </strong>
        </Typography>
        <Typography>
          Статус:{" "}
          <Chip
            label={getStatusLabel(status!)}
            color={getStatusColor(status!)}
            size="small"
          />
        </Typography>
      </div>

      {isAccepted && (
        <Alert severity="success">Ваша заявка была одобрена.</Alert>
      )}
      {isRejected && (
        <Alert severity="error">
          Ваша заявка была отклонена. Причина:{" "}
          <strong>{data.result.rejectionComment}</strong>
        </Alert>
      )}

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
        <Stack spacing={3}>
          <TextField
            label="Имя"
            {...register("firstName")}
            disabled={!isDraft}
          />
          <TextField
            label="Фамилия"
            {...register("lastName")}
            disabled={!isDraft}
          />
          <TextField
            label="Отчество"
            {...register("middleName")}
            disabled={!isDraft}
          />
          <TextField
            label="Комментарий"
            multiline
            minRows={3}
            fullWidth
            {...register("comment")}
            error={!!errors.comment}
            helperText={errors.comment?.message}
            disabled={!isDraft}
          />
        </Stack>

        {isDraft && (
          <Stack direction="row" spacing={2}>
            <Button type="submit" variant="contained" disabled={isSaving}>
              {isSaving ? "Сохранение..." : "Сохранить"}
            </Button>
            <Button
              variant="outlined"
              onClick={handleSend}
              disabled={isSubmitting}
            >
              {isSubmitting ? "Отправка..." : "Отправить"}
            </Button>
          </Stack>
        )}
      </form>

      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
      >
        <Alert
          severity={snackbar.severity}
          onClose={() => setSnackbar({ ...snackbar, open: false })}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
