import { createSlice } from "@reduxjs/toolkit/react";

export type CourseId = string;

export type Course = {
  id: CourseId;
  title: string;
  description: string;
  preview: string;
};

export type CoursesState = {
  courses: Course[];
};

const initialState: CoursesState = {
  courses: [],
};

export const coursesSlice = createSlice({
  name: "courses",
  initialState,
  reducers: {},
});

export default coursesSlice.reducer;
