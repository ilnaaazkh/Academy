import { RouteObject } from "react-router";
import { ProtectedRoute } from "../../../shared/ProtectedRoute";
import EditableCoursePage from "./EditableCoursePage";
import EditMainInfo from "./EditMainInfo";
import EditableLessonContent from "./EditableLessonContent";
import { ModerateCoursePage } from "./ModerateCoursePage";
import Lesson from "../../courses/Lesson";
import ModerateMainInfo from "./ModerateMainInfo";

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

export const moderateCourseRoutes: RouteObject = {
  path: "moderate/:id",
  Component: () => (
    <ProtectedRoute roles={["Admin"]}>
      <ModerateCoursePage />
    </ProtectedRoute>
  ),
  children: [
    {
      index: true,
      Component: ModerateMainInfo,
    },
    {
      path: "lessons/:lessonId",
      Component: Lesson,
    },
  ],
} as RouteObject;
