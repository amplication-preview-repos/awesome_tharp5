import { Module, forwardRef } from "@nestjs/common";
import { AuthModule } from "../auth/auth.module";
import { ClaimModuleBase } from "./base/claim.module.base";
import { ClaimService } from "./claim.service";
import { ClaimController } from "./claim.controller";
import { ClaimResolver } from "./claim.resolver";

@Module({
  imports: [ClaimModuleBase, forwardRef(() => AuthModule)],
  controllers: [ClaimController],
  providers: [ClaimService, ClaimResolver],
  exports: [ClaimService],
})
export class ClaimModule {}
