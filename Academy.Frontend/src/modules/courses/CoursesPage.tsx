import { Alert, CircularProgress, Grid } from "@mui/material";
import { CourseCard } from "./CourseCard";
import { useGetCoursesQuery } from "./api";
import { useState } from "react";

export default function CoursesPage() {
  const [pageNumber, setPageNumber] = useState<number>(1);
  const pageSize: number = 10;
  const { data, isError, isLoading } = useGetCoursesQuery({
    pageSize,
    pageNumber,
  });

  if (isLoading) {
    return (
      <div className="h-3/4 flex justify-center items-center">
        <CircularProgress size="3rem" />
      </div>
    );
  }

  if (isError) {
    return (
      <div className="h-3/4 flex justify-center items-center m-10">
        <Alert severity="error" variant="outlined">
          Ошибка при получении данных. Попробуйте позже
        </Alert>
      </div>
    );
  }

  return (
    <Grid
      container
      spacing={3}
      sx={{
        padding: 3,
        maxWidth: "lg",
        margin: "0 auto",
      }}
    >
      {data?.result?.map((course) => (
        <Grid
          key={course.id}
          xs={12}
          sm={6}
          md={4}
          lg={3}
          sx={{
            display: "flex",
            flexDirection: "column",
          }}
        >
          <CourseCard course={course} />
        </Grid>
      ))}
    </Grid>
  );
}
