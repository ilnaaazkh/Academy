import { useState } from "react";
import { Button, TextField, Typography, Paper } from "@mui/material";
import { useLoginMutation } from "../modules/auth/api";
import { useAppDispatch } from "../store/store";
import { setCredentials } from "../modules/auth/authSlice";
import { useNavigate } from "react-router";

function LoginForm() {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const [login, { isLoading, isError }] = useLoginMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!email || !password) {
      setError("Пожалуйста, заполните все поля.");
      return;
    }

    const response = await login({ email, password }).unwrap();

    if (response?.result) {
      dispatch(setCredentials({ accessToken: response.result.accessToken }));
      navigate("/");
    }

    setError(null);
  };

  return (
    <Paper elevation={3} className="p-6 max-w-md mx-auto">
      <Typography variant="h4" className="mb-5 text-center">
        Вход
      </Typography>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <TextField
          label="Email"
          type="email"
          fullWidth
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <TextField
          label="Пароль"
          type="password"
          fullWidth
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        {error && <Typography color="error">{error}</Typography>}
        <Button variant="contained" type="submit" fullWidth>
          Войти
        </Button>
      </form>
    </Paper>
  );
}

export default LoginForm;
