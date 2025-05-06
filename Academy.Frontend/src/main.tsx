import { createRoot } from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router";
import "./App.css";
import { router } from "./app/routes";

createRoot(document.getElementById("root")!).render(
  <RouterProvider router={router} />
);
