import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  TextField,
  Button,
  Box,
  CircularProgress,
  Typography,
} from "@mui/material";
import { useRegistrationMutation } from "../modules/auth/api";
import { useNavigate } from "react-router";

const schema = z.object({
  email: z.string().email({ message: "Введите валидный email" }),
  password: z
    .string()
    .min(8, { message: "Пароль должен содержать не менее 8 символов" }),
});

type FormFields = z.infer<typeof schema>;

export default function RegistrationForm() {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm<FormFields>({
    resolver: zodResolver(schema),
  });

  const [registration] = useRegistrationMutation();
  const navigate = useNavigate();

  const onSubmit = async ({ email, password }: FormFields) => {
    try {
      console.log(email, password);
      await registration({ email, password }).unwrap();
      navigate("/login");
    } catch {
      setError("root", { message: "Аккаунт с таким email уже существует" });
    }
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="max-w-md mx-auto mt-10 space-y-6"
    >
      <Typography variant="h5" className="text-center">
        Регистрация
      </Typography>

      <TextField
        label="Email"
        fullWidth
        {...register("email")}
        error={!!errors.email}
        helperText={errors.email?.message}
        variant="outlined"
      />

      <TextField
        label="Пароль"
        fullWidth
        type="password"
        {...register("password")}
        error={!!errors.password}
        helperText={errors.password?.message}
        variant="outlined"
      />

      {errors.root?.message && (
        <div className="text-red-500">{errors.root?.message}</div>
      )}

      <Box className="text-center">
        <Button
          type="submit"
          variant="contained"
          color="primary"
          disabled={isSubmitting}
          startIcon={
            isSubmitting ? <CircularProgress size={20} color="inherit" /> : null
          }
          className="min-w-[200px] h-12"
        >
          {isSubmitting ? "Загрузка..." : "Зарегистрироваться"}
        </Button>
      </Box>
    </form>
  );
}
