import { RouteObject } from "react-router";
import { ProtectedRoute } from "../../../shared/ProtectedRoute";
import EditableCoursePage from "./EditableCoursePage";
import EditMainInfo from "./EditMainInfo";

export const courseManagementRoutes: RouteObject = {
  path: "own-courses/:courseId",
  Component: () => (
    <ProtectedRoute roles={["Author"]}>
      <EditableCoursePage />
    </ProtectedRoute>
  ),
  children: [
    {
      index: true,
      Component: EditMainInfo,
    },
  ],
} as RouteObject;
