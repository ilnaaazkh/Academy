import { Attachment } from "./Attachment";
import { AuthorRoleRequestStatus } from "./AuthorRoleRequestStatus";

export interface AuthoringResponse {
  id: string;
  comment?: string;
  rejectionComment?: string;
  status: AuthorRoleRequestStatus;
  attachments: Attachment[];
  createdAt: string;
}
