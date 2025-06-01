import {
  Alert,
  Button,
  CircularProgress,
  Grid,
  Typography,
} from "@mui/material";
import { useGetOwnCoursesQuery } from "./api";
import { EditCourseCard } from "./EditCourseCard";
import { useState } from "react";
import CreateCourseModal from "./CreateCourseModal";

export default function OwnCourses() {
  const { data, isLoading, isError } = useGetOwnCoursesQuery();
  const [isModalOpen, setIsModalOpen] = useState(false);

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
    <>
      <div className="p-4">
        <Typography variant="h3" gutterBottom>
          Созданные вами курсы
        </Typography>

        {data?.result?.length === 0 ? (
          <Typography variant="body1">
            Вы еще не создали ни одного курса
          </Typography>
        ) : (
          <Grid container spacing={3} className="justify-between">
            {data?.result?.map((course) => (
              <Grid key={course.id}>
                <EditCourseCard course={course} />
              </Grid>
            ))}
          </Grid>
        )}
        <div className="mt-10">
          <Button
            variant="contained"
            onClick={() => setIsModalOpen(true)}
            size="large"
          >
            Создать курс
          </Button>
        </div>
      </div>
      <CreateCourseModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
      />
    </>
  );
}
