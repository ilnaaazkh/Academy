import { CourseDto } from "../../../models/response/courseDto";
import { Envelope } from "../../../models/response/Envelope";
import { baseApi } from "../../../shared/api";
import { Question } from "../../courses/models/lessonDto";

function createFormData(file: File): FormData {
  const formData = new FormData();
  formData.append("files", file);
  return formData;
}

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
    addLesson: builder.mutation<
      Envelope<string>,
      {
        courseId: string;
        moduleId: string;
        title: string;
        lessonType: "LECTURE" | "PRACTICE" | "TEST";
      }
    >({
      query: ({ courseId, moduleId, title, lessonType }) => ({
        url: `courses/${courseId}/modules/${moduleId}/lessons`,
        method: "POST",
        body: { title, lessonType },
      }),
      invalidatesTags: ["Modules"],
    }),
    deleteLesson: builder.mutation<
      Envelope<void>,
      { courseId: string; moduleId: string; lessonId: string }
    >({
      query: ({ courseId, moduleId, lessonId }) => ({
        url: `courses/${courseId}/modules/${moduleId}/lessons/${lessonId}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Modules"],
    }),
    updateLessonContent: builder.mutation<
      Envelope<void>,
      {
        courseId: string;
        moduleId: string;
        lessonId: string;
        content: string;
      }
    >({
      query: ({ courseId, moduleId, lessonId, content }) => ({
        url: `/courses/${courseId}/modules/${moduleId}/lessons/${lessonId}/content`,
        method: "POST",
        body: { content },
      }),
      invalidatesTags: ["Lesson"],
    }),
    addPracticeToLesson: builder.mutation<
      Envelope<string>,
      {
        courseId: string;
        moduleId: string;
        lessonId: string;
        templateCode: string;
      }
    >({
      query: ({ courseId, moduleId, lessonId, templateCode }) => ({
        url: `courses/${courseId}/modules/${moduleId}/lessons/${lessonId}/practice`,
        method: "POST",
        body: { templateCode },
      }),
      invalidatesTags: ["Lesson"],
    }),
    addLessonAttachments: builder.mutation<
      Envelope<string[]>,
      {
        courseId: string;
        moduleId: string;
        lessonId: string;
        files: File[];
      }
    >({
      query: ({ courseId, moduleId, lessonId, files }) => {
        const formData = new FormData();
        files.forEach((file) => {
          formData.append("files", file);
        });

        return {
          url: `courses/${courseId}/modules/${moduleId}/lessons/${lessonId}/attachments`,
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Lesson"],
    }),
    removeAttachment: builder.mutation<
      Envelope<void>,
      {
        courseId: string;
        moduleId: string;
        lessonId: string;
        fileUrl: string;
      }
    >({
      query: ({ courseId, moduleId, lessonId, fileUrl }) => ({
        url: `/courses/${courseId}/modules/${moduleId}/lessons/${lessonId}/attachments/${encodeURIComponent(
          fileUrl
        )}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Lesson"],
    }),
    addTestQuestions: builder.mutation<
      Envelope<string>,
      {
        courseId: string;
        moduleId: string;
        lessonId: string;
        questions: Question[];
      }
    >({
      query: ({ courseId, moduleId, lessonId, questions }) => ({
        url: `courses/${courseId}/modules/${moduleId}/lessons/${lessonId}/test-questions`,
        method: "POST",
        body: { questions },
      }),
      invalidatesTags: ["Lesson"],
    }),
    uploadCoursePreview: builder.mutation<
      Envelope<string>,
      {
        courseId: string;
        file: File;
      }
    >({
      query: ({ courseId, file }) => ({
        url: `courses/${courseId}/preview`,
        method: "POST",
        body: createFormData(file),
      }),
      invalidatesTags: ["CourseInfo"],
    }),
    updateCourse: builder.mutation<
      Envelope<void>,
      { id: string; title: string; description: string }
    >({
      query: ({ id, title, description }) => ({
        url: `courses/${id}`,
        method: "PUT",
        body: { title, description },
      }),
      invalidatesTags: ["Courses", "CourseInfo"],
    }),
    deleteCourse: builder.mutation<Envelope<void>, { id: string }>({
      query: ({ id }) => ({
        url: `courses/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Courses"],
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
  useAddLessonMutation,
  useDeleteLessonMutation,
  useUpdateLessonContentMutation,
  useAddPracticeToLessonMutation,
  useAddLessonAttachmentsMutation,
  useRemoveAttachmentMutation,
  useAddTestQuestionsMutation,
  useUploadCoursePreviewMutation,
  useUpdateCourseMutation,
  useDeleteCourseMutation,
} = api;
