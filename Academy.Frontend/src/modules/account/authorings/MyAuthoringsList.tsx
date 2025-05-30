import BeAuthor from "./BeAuthor";

import { useGetMyAuthoringsQuery } from "./api";
import { CircularProgress, Alert, Stack } from "@mui/material";
import MyAuthoringCard from "./MyAuthoringCard";
import { useNavigate } from "react-router";

export default function MyAuthoringsList() {
  const { data, isLoading, isError } = useGetMyAuthoringsQuery();
  const navigate = useNavigate();

  if (isLoading) {
    return (
      <div className="flex justify-center mt-10">
        <CircularProgress />
      </div>
    );
  }

  if (isError) {
    return (
      <Alert severity="error" className="my-6">
        Не удалось загрузить список заявок.
      </Alert>
    );
  }

  if (!data?.result || data.result.length === 0) {
    return <BeAuthor />;
  }

  return (
    <Stack spacing={2} className="mt-4">
      {data.result.map((authoring) => (
        <MyAuthoringCard
          onClick={() => navigate(authoring.id)}
          key={authoring.id}
          createdAt={authoring.createdAt}
          status={authoring.status}
          comment={authoring.comment}
        />
      ))}
    </Stack>
  );
}
