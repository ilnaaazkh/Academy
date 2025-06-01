import { Alert, Button, CircularProgress, Typography } from "@mui/material";
import { useGetCourseInfoQuery, usePublishCourseMutation } from "./api";
import { useNavigate, useParams } from "react-router";
import { skipToken } from "@reduxjs/toolkit/query";

export default function ModerateMainInfo() {
  const { id } = useParams();
  const { data, isLoading, isError } = useGetCourseInfoQuery(
    id ? { id } : skipToken
  );
  const navigate = useNavigate();

  const [publishCourse] = usePublishCourseMutation();

  function handlePublishCourse() {
    if (!id) return;

    publishCourse({ id })
      .unwrap()
      .then(() => navigate("/profile/publish"))
      .catch(() => console.log("Ошибка при публикации курса"));
  }

  if (isLoading) {
    return <CircularProgress />;
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

  const course = data?.result;
  return (
    <>
      <div className="flex m-14 gap-6 items-stretch">
        <div className="w-1/3 m-2 flex flex-col gap-4">
          <div className="min-h-[300px] bg-slate-200 flex justify-center items-center rounded overflow-hidden">
            {course!.preview ? (
              <img
                src={course!.preview}
                alt={course!.title}
                className="object-cover w-full h-full"
              />
            ) : (
              <span className="text-center p-4 text-gray-600">
                Обложка отсутствует
              </span>
            )}
          </div>
        </div>

        <div className="w-2/3 m-2 flex flex-col justify-between gap-4">
          <div>
            <Typography variant="h4" gutterBottom>
              {course!.title}
            </Typography>
            <Typography variant="body1" className="text-justify">
              {course!.description}
            </Typography>
          </div>
        </div>
      </div>
      <div className="flex justify-between m-14">
        <Button
          variant="contained"
          color="success"
          onClick={handlePublishCourse}
        >
          Опубликовать
        </Button>
        <Button variant="contained" color="error">
          Отклонить
        </Button>
      </div>
    </>
  );
}
