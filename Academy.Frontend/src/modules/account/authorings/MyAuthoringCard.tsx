import { Card, CardContent, Typography, Chip, Button } from "@mui/material";
import { AuthorRoleRequestStatus } from "./models/AuthorRoleRequestStatus";
import { statusMap } from "./models/statusMap";

interface Props {
  comment?: string;
  createdAt: string;
  status: AuthorRoleRequestStatus;
  onClick: () => void;
}

export default function MyAuthoringCard({ createdAt, status, onClick }: Props) {
  const { label, color } = statusMap[status];

  const formattedDate = new Date(createdAt).toLocaleDateString("ru-RU", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });

  return (
    <Card variant="outlined" className="w-full">
      <CardContent className="flex justify-between items-center">
        <div>
          <Typography variant="body1" className="mb-2">
            Заявка от {formattedDate}
          </Typography>
        </div>
        <div className="flex gap-2 items-center justify-between">
          <Chip label={label} color={color} />
          <Button variant="contained" onClick={onClick}>
            Открыть
          </Button>
        </div>
      </CardContent>
    </Card>
  );
}
