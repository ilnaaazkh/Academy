import { RouteObject } from "react-router-dom";
import { AccountPage } from "./AccountPage";
import { ProtectedRoute } from "../../shared/ProtectedRoute";
import OwnCourses from "./courseManagement/OwnCourses";

export const accountRoutes: RouteObject = {
  path: "/profile",
  Component: () => (
    <ProtectedRoute roles={[]}>
      <AccountPage />
    </ProtectedRoute>
  ),
  children: [
    {
      path: "own-courses",
      element: (
        <ProtectedRoute roles={["Author"]}>
          <OwnCourses />
        </ProtectedRoute>
      ),
    },
  ],
} as RouteObject;
