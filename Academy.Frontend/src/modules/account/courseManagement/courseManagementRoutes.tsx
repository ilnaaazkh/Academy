import { RouteObject } from "react-router";
import { ProtectedRoute } from "../../../shared/ProtectedRoute";
import EditableCoursePage from "./EditableCoursePage";

export const courseManagementRoutes: RouteObject = {
  path: "own-courses/:courseId",
  Component: () => (
    <ProtectedRoute roles={["Author"]}>
      <EditableCoursePage />
    </ProtectedRoute>
  ),
} as RouteObject;
