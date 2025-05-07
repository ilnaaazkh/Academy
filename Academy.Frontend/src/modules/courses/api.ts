import { Envelope } from "../../models/response/Envelope";
import { baseApi } from "../../shared/api";
import { Course } from "./coursesSlice";

export const coursesApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourses: builder.query<
      Envelope<Course[]>,
      { pageSize: number; pageNumber: number }
    >({
      query: ({ pageSize, pageNumber }) => ({
        url: "/courses",
        params: { pageNumber, pageSize },
      }),
    }),
  }),
});

export const { useGetCoursesQuery } = coursesApi;
