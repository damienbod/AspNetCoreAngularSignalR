import { DirectApiCallComponent } from './directApiCall/directApiCall.component';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AuthModule, OidcConfigService, LogLevel } from 'angular-auth-oidc-client';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { GraphApiCallComponent } from './graphApiCall/graphApiCall.component';
import { ApplicationApiCallComponent } from './applicationApiCall/applicationApiCall.component';
import { DelegatedApiCallComponent } from './delegatedApiCall/delegatedApiCall.component';
import { AuthorizationGuard } from './authorization.guard';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
    oidcConfigService.withConfig({
            stsServer: 'https://login.microsoftonline.com/7ff95b15-dc21-4ba6-bc92-824856578fc1/v2.0',
            authWellknownEndpoint: 'https://login.microsoftonline.com/7ff95b15-dc21-4ba6-bc92-824856578fc1/v2.0',
            redirectUrl: window.location.origin,
            clientId: 'ad6b0351-92b4-4ee9-ac8d-3e76e5fd1c67',
            scope: 'openid profile email api://2b50a014-f353-4c10-aace-024f19a55569/access_as_user offline_access',
            responseType: 'code',
            silentRenew: true,
            useRefreshToken: true,
            maxIdTokenIatOffsetAllowedInSeconds: 600,
            issValidationOff: false,
            autoUserinfo: false,
            logLevel: LogLevel.Debug
    });
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavMenuComponent,
    UnauthorizedComponent,
    DirectApiCallComponent,
    GraphApiCallComponent,
    ApplicationApiCallComponent,
    DelegatedApiCallComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'directApiCall', component: DirectApiCallComponent, canActivate: [AuthorizationGuard] },
    { path: 'graphApiCall', component: GraphApiCallComponent, canActivate: [AuthorizationGuard] },
    { path: 'applicationApiCall', component: ApplicationApiCallComponent, canActivate: [AuthorizationGuard] },
    { path: 'delegatedApiCall', component: DelegatedApiCallComponent, canActivate: [AuthorizationGuard] },
    { path: 'unauthorized', component: UnauthorizedComponent },
  ], { relativeLinkResolution: 'legacy' }),
    AuthModule.forRoot(),
    HttpClientModule,
  ],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService],
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    AuthorizationGuard
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
