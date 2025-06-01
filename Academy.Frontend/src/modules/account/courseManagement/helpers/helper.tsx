import { CourseDto } from "../../../../models/response/courseDto";

export function getCourseStatusMeta(status: CourseDto["status"]) {
  switch (status) {
    case "DRAFT":
      return { label: "Черновик", color: "default" as const };
    case "UNDER_MODERATION":
      return { label: "На модерации", color: "warning" as const };
    case "PUBLISHED":
      return { label: "Опубликован", color: "success" as const };
    default:
      return { label: "Неизвестно", color: "default" as const };
  }
}
