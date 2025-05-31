import { AuthorRoleRequestStatus } from "./AuthorRoleRequestStatus";

export const statusMap: Record<
  AuthorRoleRequestStatus,
  { label: string; color: "default" | "warning" | "error" | "success" }
> = {
  Draft: { label: "Черновик", color: "default" },
  Pending: { label: "На рассмотрении", color: "warning" },
  Rejected: { label: "Отклонена", color: "error" },
  Accepted: { label: "Принята", color: "success" },
};
