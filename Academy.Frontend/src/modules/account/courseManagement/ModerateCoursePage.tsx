import { Box } from "@mui/material";
import { skipToken } from "@reduxjs/toolkit/query";
import { Outlet, useParams } from "react-router";
import { useGetCourseStructureQuery } from "../../courses/api";
import ModerateCourseSidebar from "./components/ModerateCourseSidebar";

export function ModerateCoursePage() {
  const { id } = useParams<{ id: string }>();
  const { data, isLoading, isError } = useGetCourseStructureQuery(
    id ? { id } : skipToken
  );

  if (isLoading) return <p>Загрузка...</p>;
  if (isError || !data?.result) return <p>Ошибка загрузки</p>;

  return (
    <Box display="flex" height="100vh" overflow="hidden">
      <ModerateCourseSidebar modules={data.result} id={id!} />
      <Box className="flex-1 overflow-y-auto px-4 py-4">
        <Outlet />
      </Box>
    </Box>
  );
}
