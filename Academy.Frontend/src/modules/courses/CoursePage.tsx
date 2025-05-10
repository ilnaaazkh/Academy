import { Outlet, useParams } from "react-router";
import { useGetCourseStructureQuery } from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { Box } from "@mui/material";
import CourseSidebar from "./components/CourseSidebar";

export function CoursePage() {
  const { id } = useParams<{ id: string }>();
  const { data, isLoading, isError } = useGetCourseStructureQuery(
    id ? { id } : skipToken
  );

  if (isLoading) return <p>Загрузка...</p>;
  if (isError || !data?.result) return <p>Ошибка загрузки</p>;

  return (
    <div className="flex h-screen">
      <CourseSidebar modules={data.result} id={id!} />
      <Box className="w-5/6 p-4 m-0">
        <Outlet />
      </Box>
    </div>
  );
}
