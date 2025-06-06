import { RouteObject } from "react-router-dom";
import { AccountPage } from "./AccountPage";
import { ProtectedRoute } from "../../shared/ProtectedRoute";
import OwnCourses from "./courseManagement/OwnCourses";
import AuthorsPage from "./authors/AuthorsPage";
import MyAuthoringsPage from "./authorings/MyAuthoringsPage";
import MyAuthoringPage from "./authorings/MyAuthoringPage";
import AuthoringsPage from "./authorings/AuthoringsPage";
import AuthoringDecisionPage from "./authorings/AuthoringDecisionPage";
import PublishRequests from "./courseManagement/PublishRequests";

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
    {
      path: "own-courses",
      element: (
        <ProtectedRoute roles={["Author"]}>
          <OwnCourses />
        </ProtectedRoute>
      ),
    },
    {
      path: "authors",
      element: (
        <ProtectedRoute roles={["Admin"]}>
          <AuthorsPage />
        </ProtectedRoute>
      ),
    },
    {
      path: "my-authorings",
      element: (
        <ProtectedRoute roles={["Student"]}>
          <MyAuthoringsPage />
        </ProtectedRoute>
      ),
    },
    {
      path: "authorings",
      element: (
        <ProtectedRoute roles={["Admin"]}>
          <AuthoringsPage />
        </ProtectedRoute>
      ),
    },
    {
      path: "publish",
      element: (
        <ProtectedRoute roles={["Admin"]}>
          <PublishRequests />
        </ProtectedRoute>
      ),
    },
    {
      path: "my-authorings/:id",
      element: (
        <ProtectedRoute roles={["Student"]}>
          <MyAuthoringPage />
        </ProtectedRoute>
      ),
    },
    {
      path: "authorings/:id",
      element: (
        <ProtectedRoute roles={["Admin"]}>
          <AuthoringDecisionPage />
        </ProtectedRoute>
      ),
    },
  ],
} as RouteObject;
