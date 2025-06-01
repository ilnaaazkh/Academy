import api from "../api/index";
import { AxiosResponse } from "axios";
import { AuthResponse } from "../models/response/AuthResponse";
import { Envelope } from "../models/response/Envelope";

export default class AuthService {
  static async login(
    email: string,
    password: string
  ): Promise<AxiosResponse<Envelope<AuthResponse>>> {
    return api.post("/accounts/login", { email, password });
  }

  static async register(
    email: string,
    password: string
  ): Promise<AxiosResponse<Envelope<AuthResponse>>> {
    return api.post("/accounts/register", { email, password });
  }

  static async logout(): Promise<void> {
    return Promise.resolve();
  }
}
