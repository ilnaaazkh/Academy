export interface LessonDto {
  id: string;
  title: string;
  position: number;
  content: string;
  lessonType: "TEST" | "LECTURE" | "PRACTICE";
  questions: Question[];
  attachments: Attachment[];
  practiceLessonData: PracticeLessonData;
}

export interface Question {
  title: string;
  answers: Answer[];
}

export interface Answer {
  text: string;
  isCorrect: boolean;
}

export interface Attachment {
  fileName: string;
  fileUrl: string;
}

export interface PracticeLessonData {
  templateCode: string;
  tests: Test[];
}

export interface Test {
  input: string;
  output: string;
}
