import { Card, CardContent, Typography, IconButton } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import { AuthorResponse } from "./models/AuthorResponse";

interface Props extends AuthorResponse {
  onDelete: (id: string) => void;
}

export default function AuthorCard({
  id,
  email,
  firstName,
  lastName,
  middleName,
  onDelete,
}: Props) {
  const fullName = [lastName, firstName, middleName].filter(Boolean).join(" ");

  return (
    <Card
      variant="outlined"
      className="w-full flex justify-between items-center px-4"
    >
      <div>
        <CardContent className="flex gap-3 items-center">
          <Typography variant="h6" className="font-semibold">
            {fullName}
          </Typography>
          <Typography color="text.secondary">{email}</Typography>
        </CardContent>
      </div>
      <div>
        <IconButton
          onClick={() => onDelete(id)}
          color="error"
          aria-label="Удалить автора"
        >
          <DeleteIcon />
        </IconButton>
      </div>
    </Card>
  );
}
