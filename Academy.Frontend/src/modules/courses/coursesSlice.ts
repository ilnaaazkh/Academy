import { createSlice } from "@reduxjs/toolkit/react";

export type CourseId = string;

export type Course = {
  id: CourseId;
  title: string;
  description: string;
};

export type CoursesState = {
  courses: Course[];
};

export const testCourses: Course[] = [
  {
    id: "1",
    title: "Основы C#",
    description: "Изучите базовые концепции C# и .NET платформы",
  },
  {
    id: "2",
    title: "ООП в C#",
    description: "Погружение в объектно-ориентированное программирование",
  },
  {
    id: "3",
    title: "ASP.NET Core",
    description: "Создание веб-приложений на современном стеке Microsoft",
  },
  {
    id: "4",
    title: "Entity Framework",
    description: "Работа с базами данных через ORM",
  },
  {
    id: "5",
    title: "WPF и MVVM",
    description: "Разработка desktop-приложений с паттерном MVVM",
  },
  {
    id: "6",
    title: "Unit-тестирование",
    description: "NUnit, xUnit и Moq для тестирования кода",
  },
  {
    id: "7",
    title: "Docker для .NET",
    description: "Контейнеризация .NET приложений",
  },
  {
    id: "8",
    title: "Микросервисы",
    description: "Архитектура микросервисов на .NET",
  },
];

const initialState: CoursesState = {
  courses: testCourses,
};

export const coursesSlice = createSlice({
  name: "courses",
  initialState,
  reducers: {},
});

export default coursesSlice.reducer;
