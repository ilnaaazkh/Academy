export type CourseId = string;

export type CourseDto = {
  id: CourseId;
  title: string;
  description: string;
  status: "DRAFT" | "UNDER_MODERATION" | "PUBLISHED";
  preview: string;
};
