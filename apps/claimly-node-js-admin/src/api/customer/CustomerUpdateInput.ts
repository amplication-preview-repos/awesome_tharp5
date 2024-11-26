import { ClaimUpdateManyWithoutCustomersInput } from "./ClaimUpdateManyWithoutCustomersInput";

export type CustomerUpdateInput = {
  claims?: ClaimUpdateManyWithoutCustomersInput;
  email?: string | null;
  name?: string | null;
  phone?: string | null;
};
