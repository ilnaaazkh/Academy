import { RouteObject } from "react-router-dom";
import CoursesPage from "./CoursesPage";
import { CoursePage } from "./CoursePage";
import Lesson from "./Lesson";

export const coursesRoutes = {
  index: true,
  Component: CoursesPage,
} as RouteObject;

const lessonRoute = {
  path: "lessons/:lessonId",
  Component: Lesson,
} as RouteObject;

export const courseRoute = {
  path: ":id",
  Component: CoursePage,
  children: [lessonRoute],
} as RouteObject;
