import { Claim } from "../claim/Claim";

export type ClaimStatus = {
  claims?: Array<Claim>;
  createdAt: Date;
  id: string;
  status: string | null;
  updatedAt: Date;
};
