import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { Button, CircularProgress, TextField, Typography } from "@mui/material";
import BaseModal from "../../../components/BaseModal";
import { useCreateAuthorMutation } from "./api";

interface Props {
  isOpen: boolean;
  onClose: () => void;
}

const schema = z.object({
  firstName: z.string().nonempty("Поле обязательно для заполнения"),
  lastName: z.string().nonempty("Поле обязательно для заполнения"),
  middleName: z.string().nonempty("Поле обязательно для заполнения"),
  email: z.string().email("Введите корректный email"),
  password: z.string().min(8, "Минимум 8 символов"),
});

type FormFields = z.infer<typeof schema>;

export default function RegisterAuthorModal({ isOpen, onClose }: Props) {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
    setError,
  } = useForm<FormFields>({ resolver: zodResolver(schema) });

  const [createAuthor] = useCreateAuthorMutation();

  async function onSubmit(data: FormFields) {
    try {
      await createAuthor(data).unwrap();
      onCloseWrapper();
    } catch {
      setError("root", {
        message: "Не удалось зарегистрировать автора",
        type: "manual",
      });
    }
  }

  function onCloseWrapper() {
    reset();
    onClose();
  }

  return (
    <BaseModal
      open={isOpen}
      onClose={onCloseWrapper}
      title="Регистрация автора"
    >
      <form
        className="flex flex-col gap-3"
        onSubmit={handleSubmit(onSubmit)}
        autoComplete="off"
      >
        <TextField
          label="Фамилия"
          type="text"
          autoComplete="new-last-name"
          {...register("lastName")}
          error={!!errors.lastName}
          helperText={errors.lastName?.message}
        />

        <TextField
          label="Имя"
          type="text"
          autoComplete="new-first-name"
          {...register("firstName")}
          error={!!errors.firstName}
          helperText={errors.firstName?.message}
        />

        <TextField
          label="Отчество"
          type="text"
          autoComplete="new-middle-name"
          {...register("middleName")}
          error={!!errors.middleName}
          helperText={errors.middleName?.message}
        />

        <TextField
          label="Email"
          type="email"
          autoComplete="new-email"
          {...register("email")}
          error={!!errors.email}
          helperText={errors.email?.message}
        />

        <TextField
          label="Пароль"
          type="password"
          autoComplete="new-password"
          {...register("password")}
          error={!!errors.password}
          helperText={errors.password?.message}
        />

        {errors.root?.message && (
          <Typography color="error">{errors.root.message}</Typography>
        )}

        <Button
          variant="contained"
          type="submit"
          disabled={isSubmitting}
          startIcon={isSubmitting ? <CircularProgress size={20} /> : null}
        >
          {isSubmitting ? "Загрузка..." : "Зарегистрировать"}
        </Button>
      </form>
    </BaseModal>
  );
}
