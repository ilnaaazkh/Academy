import { Card, CardContent, Typography, Button } from "@mui/material";
import { AuthoringResponse } from "./models/AuthoringResponse";

interface Props {
  authoring: AuthoringResponse;
  onClick: () => void;
}

export default function AuthoringCard({ authoring, onClick }: Props) {
  return (
    <Card variant="outlined" className="w-full">
      <CardContent className="flex justify-between items-center">
        <div>
          <Typography variant="body1" className="mb-2">
            {authoring.lastName} {authoring.firstName} {authoring.middleName}
          </Typography>
        </div>
        <Button variant="contained" onClick={onClick}>
          Открыть
        </Button>
      </CardContent>
    </Card>
  );
}
