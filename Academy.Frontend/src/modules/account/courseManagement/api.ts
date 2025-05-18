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
    addModule: builder.mutation<
      Envelope<string>,
      { title: string; courseId: string }
    >({
      query: ({ title, courseId }) => ({
        url: `courses/${courseId}/modules`,
        method: "POST",
        body: { title },
      }),
      invalidatesTags: ["Modules"],
    }),
    deleteModule: builder.mutation<
      Envelope<void>,
      { courseId: string; moduleId: string }
    >({
      query: ({ courseId, moduleId }) => ({
        url: `courses/${courseId}/modules/${moduleId}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Modules"],
    }),
    updateModule: builder.mutation<
      Envelope<string>,
      {
        courseId: string;
        moduleId: string;
        title: string;
      }
    >({
      query: ({ courseId, moduleId, title }) => ({
        url: `courses/${courseId}/modules/${moduleId}`,
        method: "PUT",
        body: { title },
      }),
      invalidatesTags: ["Modules"],
    }),
    getCourseInfo: builder.query<Envelope<CourseDto>, { id: string }>({
      query: ({ id }) => ({
        url: `courses/${id}`,
        method: "GET",
      }),
      providesTags: ["CourseInfo"],
    }),
  }),
});

export const {
  useGetOwnCoursesQuery,
  useCreateCourseMutation,
  useAddModuleMutation,
  useDeleteModuleMutation,
  useUpdateModuleMutation,
  useGetCourseInfoQuery,
} = api;
