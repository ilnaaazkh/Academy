import { Grid } from "@mui/material";
import { CourseCard } from "./CourseCard";
import { useGetCoursesQuery } from "./api";

export default function CoursesPage() {
  const { data, isError, isLoading } = useGetCoursesQuery();

  if (isLoading) {
    return <div>Загрузка...</div>;
  }

  if (isError) {
    return <div>Ошибка</div>;
  }

  {
    data?.toString();
  }
}
