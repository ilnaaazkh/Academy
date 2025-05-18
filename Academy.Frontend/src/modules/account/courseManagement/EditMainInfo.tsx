import { useParams } from "react-router";
import { useGetCourseInfoQuery } from "./api";
import { skipToken } from "@reduxjs/toolkit/query";

export default function EditMainInfo() {
  const { courseId } = useParams();
  const { data, isFetching, isError } = useGetCourseInfoQuery(
    courseId ? { id: courseId } : skipToken
  );

  return <h1>dkjcnsdkjncsd</h1>;
}
