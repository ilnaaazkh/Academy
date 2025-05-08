import { RouteObject } from "react-router-dom";
import CoursesPage from "./CoursesPage";
import { CoursePage } from "./CoursePage";

export const coursesRoutes = {
  index: true,
  Component: CoursesPage,
} as RouteObject;

export const courseRoute = {
  path: ":id",
  Component: CoursePage,
} as RouteObject;
