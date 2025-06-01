import { Alert, CircularProgress, Grid } from "@mui/material";
import { CourseCard } from "./CourseCard";
import { useGetCoursesQuery } from "./api";
import { useState } from "react";

export default function CoursesPage() {
  const [pageNumber] = useState<number>(1);
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
      className="justify-between mx-20 mt-20"
      columns={{ xs: 4, sm: 8, md: 12 }}
    >
      {data?.result?.map((course) => (
        <Grid key={course.id} className="my-2">
          <CourseCard course={course} />
        </Grid>
      ))}
    </Grid>
  );
}
