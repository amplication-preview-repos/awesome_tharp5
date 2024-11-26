import * as graphql from "@nestjs/graphql";
import * as nestAccessControl from "nest-access-control";
import * as gqlACGuard from "../auth/gqlAC.guard";
import { GqlDefaultAuthGuard } from "../auth/gqlDefaultAuth.guard";
import * as common from "@nestjs/common";
import { ClaimStatusResolverBase } from "./base/claimStatus.resolver.base";
import { ClaimStatus } from "./base/ClaimStatus";
import { ClaimStatusService } from "./claimStatus.service";

@common.UseGuards(GqlDefaultAuthGuard, gqlACGuard.GqlACGuard)
@graphql.Resolver(() => ClaimStatus)
export class ClaimStatusResolver extends ClaimStatusResolverBase {
  constructor(
    protected readonly service: ClaimStatusService,
    @nestAccessControl.InjectRolesBuilder()
    protected readonly rolesBuilder: nestAccessControl.RolesBuilder
  ) {
    super(service, rolesBuilder);
  }
}
