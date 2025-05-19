import { RouteObject } from "react-router";
import { ProtectedRoute } from "../../../shared/ProtectedRoute";
import EditableCoursePage from "./EditableCoursePage";
import EditMainInfo from "./EditMainInfo";
import EditableLessonContent from "./EditableLessonContent";

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
    {
      path: "modules/:moduleId/lessons/:lessonId",
      Component: EditableLessonContent,
    },
  ],
} as RouteObject;
