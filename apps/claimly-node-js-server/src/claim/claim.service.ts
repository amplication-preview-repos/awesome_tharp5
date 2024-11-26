import { Injectable } from "@nestjs/common";
import { PrismaService } from "../prisma/prisma.service";
import { ClaimServiceBase } from "./base/claim.service.base";

@Injectable()
export class ClaimService extends ClaimServiceBase {
  constructor(protected readonly prisma: PrismaService) {
    super(prisma);
  }
}
