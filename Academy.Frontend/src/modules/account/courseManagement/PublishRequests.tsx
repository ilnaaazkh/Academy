import { useGetCoursesUnderModerationQuery } from "./api";
import { Alert, Box, CircularProgress, Grid, Typography } from "@mui/material";
import { ModerateCourseCard } from "./components/ModerateCourseCard";

export default function PublishRequests() {
  const { data, isLoading, isError } = useGetCoursesUnderModerationQuery();

  if (isLoading) {
    return (
      <Box className="flex justify-center mt-10">
        <CircularProgress />
      </Box>
    );
  }

  if (isError || !data?.result?.length) {
    return (
      <Alert severity="info" className="my-10">
        Курсы на модерации не найдены.
      </Alert>
    );
  }

  return (
    <Box className="p-6">
      <Typography variant="h4" gutterBottom>
        Курсы на модерации
      </Typography>

      <Grid
        container
        className="justify-between"
        spacing={3}
        columns={{ xs: 4, sm: 8, md: 12 }}
      >
        {data.result.map((course) => (
          <Grid key={course.id} size={4}>
            <ModerateCourseCard course={course} />
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
