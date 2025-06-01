import {
  Alert,
  Box,
  Button,
  CircularProgress,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import {
  useApproveAuthoringMutation,
  useGetAuthoringQuery,
  useRejectAuthoringMutation,
} from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { useState } from "react";

export default function AuthoringDecisionPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const { data, isLoading, isError } = useGetAuthoringQuery(
    id ? { id } : skipToken
  );

  const [rejectReason, setRejectReason] = useState("");
  const [approve, { isLoading: isApproving }] = useApproveAuthoringMutation();
  const [reject, { isLoading: isRejecting }] = useRejectAuthoringMutation();

  const [errorMessage, setErrorMessage] = useState("");
  const [validationError, setValidationError] = useState("");

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

  const handleApprove = async () => {
    setErrorMessage("");
    try {
      await approve({ authoringId: id! }).unwrap();
      navigate("/profile/authorings");
    } catch {
      setErrorMessage("Ошибка при одобрении заявки");
    }
  };

  const handleReject = async () => {
    setErrorMessage("");
    setValidationError("");

    if (!rejectReason.trim()) {
      setValidationError("Укажите причину отклонения");
      return;
    }

    try {
      await reject({ authoringId: id!, reason: rejectReason }).unwrap();
      navigate("/profile/authorings");
    } catch {
      setErrorMessage("Ошибка при отклонении заявки");
    }
  };

  const { firstName, lastName, middleName } = data.result;

  return (
    <Box className="p-6 space-y-6">
      <Typography variant="h4">Заявка на получение роли автора</Typography>

      <Typography variant="body1" className="flex items-start">
        ФИО:{" "}
        <strong>
          {lastName} {firstName} {middleName}
        </strong>
      </Typography>

      <Typography variant="body1" className="flex items-start">
        Комментарий пользователя:{" "}
        <strong>{data.result.comment || "Не указан"}</strong>
      </Typography>

      {errorMessage && <Alert severity="error">{errorMessage}</Alert>}
      {validationError && <Alert severity="warning">{validationError}</Alert>}

      <TextField
        label="Причина отклонения"
        multiline
        minRows={3}
        fullWidth
        value={rejectReason}
        onChange={(e) => setRejectReason(e.target.value)}
      />

      <Stack direction="row" spacing={2}>
        <Button
          variant="contained"
          color="success"
          onClick={handleApprove}
          disabled={isApproving}
        >
          {isApproving ? "Одобрение..." : "Одобрить"}
        </Button>
        <Button
          variant="contained"
          color="error"
          onClick={handleReject}
          disabled={isRejecting}
        >
          {isRejecting ? "Отклонение..." : "Отклонить"}
        </Button>
      </Stack>
    </Box>
  );
}
