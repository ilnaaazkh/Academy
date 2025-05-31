export function getStatusColor(
  status: string
): "default" | "warning" | "success" | "error" {
  switch (status) {
    case "Draft":
      return "default";
    case "Pending":
      return "warning";
    case "Accepted":
      return "success";
    case "Rejected":
      return "error";
    default:
      return "default";
  }
}

export function getStatusLabel(status: string): string {
  switch (status) {
    case "Draft":
      return "Черновик";
    case "Pending":
      return "На рассмотрении";
    case "Accepted":
      return "Принято";
    case "Rejected":
      return "Отклонено";
    default:
      return status;
  }
}
