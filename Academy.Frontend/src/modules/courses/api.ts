import { CourseDto } from "../../models/response/courseDto";
import { Envelope } from "../../models/response/Envelope";
import { baseApi } from "../../shared/api";
import { LessonDto } from "./models/lessonDto";
import { ModuleDto } from "./models/moduleDto";

export const coursesApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourses: builder.query<
      Envelope<CourseDto[]>,
      { pageSize: number; pageNumber: number }
    >({
      query: ({ pageSize, pageNumber }) => ({
        url: "/courses",
        params: { pageNumber, pageSize },
      }),
    }),
    getCourseStructure: builder.query<Envelope<ModuleDto[]>, { id: string }>({
      query: ({ id }) => ({
        url: `/courses/${id}/modules`,
      }),
      providesTags: ["Modules"],
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
    runCode: builder.mutation<Envelope<string>, { code: string }>({
      query: ({ code }) => ({
        url: "/execute",
        method: "POST",
        body: { code },
      }),
    }),
  }),
});

export const {
  useGetCoursesQuery,
  useGetCourseStructureQuery,
  useGetLessonQuery,
  useLazyGetAttachmentLinkQuery,
  useRunCodeMutation,
} = coursesApi;
