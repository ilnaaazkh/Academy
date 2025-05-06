import { baseApi } from "../../shared/api";
import { Course } from "./coursesSlice";

export const coursesApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourses: builder.query<Course[], void>({
      query: () => ({
        url: "/courses",
      }),
    }),
  }),
});

export const { useGetCoursesQuery } = coursesApi;
