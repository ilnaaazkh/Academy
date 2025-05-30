import { baseApi } from "../../../shared/api";
import { Envelope } from "../../../models/response/Envelope";
import { RegisterAuthorRequest } from "./models/RegisterAuthorRequest";
import { AuthorResponse } from "./models/AuthorResponse";

export const authorsApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createAuthor: builder.mutation<Envelope<string>, RegisterAuthorRequest>({
      query: (body) => ({
        url: "authors",
        method: "POST",
        body,
      }),
      invalidatesTags: ["Authors"],
    }),
    getAuthors: builder.query<Envelope<AuthorResponse[]>, { search?: string }>({
      query: ({ search }) => ({
        url: "authors",
        method: "GET",
        params: search ? { search } : {},
      }),
      providesTags: ["Authors"],
    }),
    deleteAuthor: builder.mutation<Envelope<void>, string>({
      query: (id) => ({
        url: `authors/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Authors"],
    }),
  }),
});

export const {
  useCreateAuthorMutation,
  useGetAuthorsQuery,
  useDeleteAuthorMutation,
} = authorsApi;
