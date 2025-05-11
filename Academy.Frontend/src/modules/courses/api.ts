import { Envelope } from "../../models/response/Envelope";
import { baseApi } from "../../shared/api";
import { Course } from "./coursesSlice";
import { LessonDto } from "./models/lessonDto";
import { ModuleDto } from "./models/moduleDto";

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
    getCourseStructure: builder.query<Envelope<ModuleDto[]>, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}`,
      }),
    }),
    getLesson: builder.query<Envelope<LessonDto>, { id: string }>({
      query: ({ id }) => ({
        url: `/lessons/${id}`,
      }),
    }),
    getAttachmentLink: builder.query<
      Envelope<string>,
      { lessonId: string; fileUrl: string }
    >({
      query: ({ lessonId, fileUrl }) => ({
        url: `/lessons/${lessonId}/attachments/${encodeURIComponent(fileUrl)}`,
      }),
    }),
  }),
});

export const {
  useGetCoursesQuery,
  useGetCourseStructureQuery,
  useGetLessonQuery,
  useLazyGetAttachmentLinkQuery,
} = coursesApi;
