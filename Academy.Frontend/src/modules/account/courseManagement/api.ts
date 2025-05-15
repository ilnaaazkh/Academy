import { CourseDto } from "../../../models/response/courseDto";
import { Envelope } from "../../../models/response/Envelope";
import { baseApi } from "../../../shared/api";

const api = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getOwnCourses: builder.query<Envelope<CourseDto[]>, void>({
      query: () => ({
        url: "courses/my-courses",
        method: "GET",
      }),
      providesTags: ["Courses"],
    }),
    createCourse: builder.mutation<
      Envelope<string>,
      { title: string; description: string }
    >({
      query: ({ title, description }) => ({
        url: "courses",
        method: "POST",
        body: { title, description },
      }),
      invalidatesTags: ["Courses"],
    }),
  }),
});

export const { useGetOwnCoursesQuery, useCreateCourseMutation } = api;
