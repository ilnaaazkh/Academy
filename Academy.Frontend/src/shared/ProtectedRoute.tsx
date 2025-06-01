import { JSX } from "react";
import { useAppSelector } from "../store/store";
import {
  Role,
  selectIsAuthenticated,
  selectRoles,
} from "../modules/auth/authSlice";
import { Navigate } from "react-router-dom";

interface Props {
  children: JSX.Element;
  roles?: Role[];
}

export function ProtectedRoute({ children, roles = [] }: Props) {
  const isAuthenticated = useAppSelector(selectIsAuthenticated);
  const userRoles = useAppSelector(selectRoles);

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  if (roles.length > 0 && !roles.some((role) => userRoles.includes(role))) {
    return <Navigate to="/login" replace />;
  }

  return children;
}
