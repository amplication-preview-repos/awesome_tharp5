import { ClaimStatus } from "../claimStatus/ClaimStatus";
import { Customer } from "../customer/Customer";

export type Claim = {
  amount: number | null;
  claimStatus?: ClaimStatus | null;
  createdAt: Date;
  customer?: Customer | null;
  date: Date | null;
  description: string | null;
  id: string;
  updatedAt: Date;
};
