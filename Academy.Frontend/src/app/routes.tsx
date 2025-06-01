import { createBrowserRouter } from "react-router";
import App from "../App";
import { LoginPage } from "../pages/LoginPage";
import RegistrationPage from "../pages/RegisterPage";
import { courseRoute, coursesRoutes } from "../modules/courses/сoursesRoutes";
import { accountRoutes } from "../modules/account/accountRoutes";
import {
  courseManagementRoutes,
  moderateCourseRoutes,
} from "../modules/account/courseManagement/courseManagementRoutes";

export const router = createBrowserRouter([
  {
    path: "/",
    Component: App,
    children: [
      {
        path: "/login",
        Component: LoginPage,
      },
      {
        path: "/register",
        Component: RegistrationPage,
      },
      coursesRoutes,
      accountRoutes,
      courseRoute,
      courseManagementRoutes,
      moderateCourseRoutes,
    ],
  },
]);
