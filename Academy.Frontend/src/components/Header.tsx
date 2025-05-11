import { AppBar, Button } from "@mui/material";
import { useSelector } from "react-redux";
import { Link as RouterLink } from "react-router-dom";
import { selectIsAuthenticated } from "../modules/auth/authSlice";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";

export default function Header() {
  const isAuthenticated = useSelector(selectIsAuthenticated);

  return (
    <AppBar position="static">
      <div className="flex items-center justify-between px-6 py-4 shadow-md bg-white">
        <RouterLink to="/" className="text-2xl font-bold text-blue-600">
          C# Academy
        </RouterLink>
        {isAuthenticated ? (
          <RouterLink to="/login">
            <AccountCircleIcon color="disabled" />
          </RouterLink>
        ) : (
          <div className="flex gap-4">
            <Button
              variant="contained"
              color="primary"
              component={RouterLink}
              to="/login"
            >
              Вход
            </Button>
            <Button
              variant="contained"
              color="primary"
              component={RouterLink}
              to="/register"
            >
              Регистрация
            </Button>
          </div>
        )}
      </div>
    </AppBar>
  );
}
