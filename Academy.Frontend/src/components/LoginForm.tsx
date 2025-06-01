import {
  Button,
  TextField,
  Typography,
  Box,
  CircularProgress,
} from "@mui/material";
import { useLoginMutation } from "../modules/auth/api";
import { useAppDispatch } from "../store/store";
import { setCredentials } from "../modules/auth/authSlice";
import { useNavigate } from "react-router";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

const schema = z.object({
  email: z.string().nonempty({ message: "Поле обязательно для заполения" }),
  password: z.string().nonempty({ message: "Поле обязательно для заполения" }),
});

type FormFields = z.infer<typeof schema>;

function LoginForm() {
  const {
    register,
    handleSubmit,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({
    resolver: zodResolver(schema),
  });
  const [login] = useLoginMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const onSubmit = async ({ email, password }: FormFields) => {
    try {
      const response = await login({ email, password }).unwrap();

      if (response?.result) {
        dispatch(
          setCredentials({
            accessToken: response.result.accessToken,
            roles: response.result.roles,
          })
        );
        navigate("/");
      }
    } catch {
      setError("root", {
        message: "Неверный логин или пароль",
      });
    }
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="max-w-md mx-auto mt-10 space-y-6"
    >
      <Typography variant="h5" className="text-center">
        Вход в аккаунт
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
          {isSubmitting ? "Загрузка..." : "Войти"}
        </Button>
      </Box>
    </form>
  );
}

export default LoginForm;
