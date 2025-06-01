import { baseApi } from "../../../shared/api";
import { Envelope } from "../../../models/response/Envelope";
import { CreateAuthoringRequest } from "./models/CreateAuthoringRequest";
import { AuthoringResponse } from "./models/AuthoringResponse";

const api = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createAuthoring: builder.mutation<Envelope<string>, CreateAuthoringRequest>(
      {
        query: (body) => ({
          url: "authorings",
          method: "POST",
          body,
        }),
        invalidatesTags: ["MyAuthorings"],
      }
    ),

    submitAuthoring: builder.mutation<Envelope<void>, { authoringId: string }>({
      query: ({ authoringId }) => ({
        url: `authorings/${authoringId}/submit`,
        method: "POST",
      }),
      invalidatesTags: ["MyAuthorings"],
    }),

    approveAuthoring: builder.mutation<Envelope<void>, { authoringId: string }>(
      {
        query: ({ authoringId }) => ({
          url: `authorings/${authoringId}/approve`,
          method: "POST",
        }),
        invalidatesTags: ["Authorings", "Authors"],
      }
    ),

    rejectAuthoring: builder.mutation<
      Envelope<void>,
      { authoringId: string; reason: string }
    >({
      query: ({ authoringId, reason }) => ({
        url: `authorings/${authoringId}/reject`,
        method: "POST",
        body: { reason },
      }),
      invalidatesTags: ["Authorings"],
    }),

    uploadAuthoringFiles: builder.mutation<
      Envelope<string[]>,
      { authoringId: string; files: File[] }
    >({
      query: ({ authoringId, files }) => {
        const formData = new FormData();
        files.forEach((file) => formData.append("files", file));
        return {
          url: `authorings/${authoringId}/files`,
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Authorings"],
    }),

    getAttachmentDownloadLink: builder.query<
      Envelope<string>,
      { authoringId: string; fileId: string }
    >({
      query: ({ authoringId, fileId }) => ({
        url: `authorings/${authoringId}/files/${fileId}`,
      }),
    }),

    getAuthorings: builder.query<
      Envelope<AuthoringResponse[]>,
      CreateAuthoringRequest
    >({
      query: (params) => ({
        url: "authorings",
        params,
      }),
      providesTags: ["Authorings"],
    }),
    getMyAuthorings: builder.query<Envelope<AuthoringResponse[]>, void>({
      query: () => ({
        url: "authorings/my-authorings",
        method: "GET",
      }),
      providesTags: ["MyAuthorings"],
    }),
    getAuthoring: builder.query<Envelope<AuthoringResponse>, { id: string }>({
      query: ({ id }) => ({
        url: `authorings/${id}`,
        method: "GET",
      }),
      providesTags: ["MyAuthorings"],
    }),
    updateAuthoring: builder.mutation<
      Envelope<string>,
      {
        id: string;
        comment?: string;
        firstName?: string;
        lastName?: string;
        middleName?: string;
      }
    >({
      query: ({ id, ...body }) => ({
        url: `authorings/${id}`,
        method: "PUT",
        body,
      }),
      invalidatesTags: ["MyAuthorings"],
    }),
  }),
});

export const {
  useCreateAuthoringMutation,
  useSubmitAuthoringMutation,
  useApproveAuthoringMutation,
  useRejectAuthoringMutation,
  useUploadAuthoringFilesMutation,
  useGetAttachmentDownloadLinkQuery,
  useGetAuthoringsQuery,
  useGetMyAuthoringsQuery,
  useGetAuthoringQuery,
  useUpdateAuthoringMutation,
} = api;
