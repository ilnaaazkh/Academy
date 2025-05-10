export interface ModuleDto {
  id: string;
  courseId: string;
  title: string;
  position: number;
  lessons: LessonInfoDto[];
}

export interface LessonInfoDto {
  id: string;
  title: string;
  position: number;
  lessonType: string;
}
