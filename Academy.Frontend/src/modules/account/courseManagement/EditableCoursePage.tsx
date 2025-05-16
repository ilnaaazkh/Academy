import { Outlet, useParams } from "react-router";
import EditableCourseSidebar from "./EditableCourseSidebar";
import { Box } from "@mui/material";
import { useGetCourseStructureQuery } from "../../courses/api";
import { skipToken } from "@reduxjs/toolkit/query";

export default function EditableCoursePage() {
  const { courseId } = useParams();
  const { data, isLoading, isError } = useGetCourseStructureQuery(
    courseId ? { id: courseId } : skipToken
  );

  if (isLoading) return <p>Загрузка...</p>;
  if (isError || !data?.result) return <p>Ошибка загрузки</p>;

  return (
    <Box display="flex" height="100vh" overflow="hidden">
      <EditableCourseSidebar modules={data.result} id={courseId!} />
      <Box className="flex-1 overflow-y-auto px-4 py-4">
        <Outlet />
      </Box>
    </Box>
  );
}
