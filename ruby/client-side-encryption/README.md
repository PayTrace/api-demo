api-demo
========

A Ruby on Rails demonstration of the new PayTrace JSON API and how to use
PayTrace's Client-Side Encryption library.

Getting Access to the PayTrace API
==================================

To access the PayTrace API, you must have a PayTrace Professional or PayTrace Cash Advance account
set up and have the API enabled for your account.  If you have an account but the API is not enabled
on it, please contact your merchant service provider.

api.paytrace.com uses [OAuth 2.0][] for authentication and authorization.  We use
the "resource owner password credentials" method of authorization grant, where the
"resource owner" is a PayTrace API account set up from our [Virtual Terminal][].  Therefore,
there is no client identifier or client secret for accessing this API.  The OAuth 2.0
token endpoint is https://api.paytrace.com/oauth/token (but you'll just get a 404 if
you click on it).  If this paragraph sounds scary, take a look at the code in
`ApplicationController.paytrace_api` in `app/controllers/application_controller.rb`
and you'll see that (with a good OAuth 2.0 library to help you) it's really not that hard.

Every request that is part of the API proper requires authentication via the
access token described in the previous paragraph.  <sup>[1](#user-content-fn1)</sup>

Setting Up a Shopping Cart (Ruby on Rails)
==========================================

Here is a walk-through of how I built this demonstration cart that uses the
PayTrace API.  It follows roughly with the commits for the master branch.

## Create a New Rails Project

    rails new api-demo

## Tweak Settings

Most teams have their favorite practices for managing a Rails project.  Our team likes to
provide a `Vagrantfile` and shell provisioning script so that a new team member on the
project (who has [Vagrant][] and [VirtualBox][] installed) can just `vagrant up` and be ready to
work.

## Set Up Access to the PayTrace API

(In `app/controllers/application_controller.rb`)

I put the required token-acquisition code in the ApplicationController class so that
any Controller class in the project has access to it as `paytrace_api`.  It can also
be accessed by other code (e.g. rake tasks) as `ApplicationController.paytrace_api`.
I stored credentials for the demonstration account in the "development" environment
configuration.  This way, it isn't possible for them to be used by a production server.

Putting the access code in ApplicationController is just a way to "git 'er done."  Please
put the access code wherever it makes sense for your application.

## Generate a Controller for the Shopping Cart

The shopping cart controller should have "checkout" and "pay" actions:

    rails generate controller cart checkout pay

The "checkout" action will display a form for capturing the required credit card
fields for payment, and the "pay" action will carry out the sale transaction authorization
via the PayTrace API and show the results on a page.

Adjust the routing for the "pay" action (in `config/routes.rb`) to allow only HTTP POST.

## Process the Sale in CartController#pay

(In `app/controllers/cart_controller.rb`)

For sake of example, let's work on the assumption that `CartController#pay` will receive
the values we need to collect from the user (to process the sale transaction) in
`params[:sale]`.  This demonstration explicitly sets the amount of the transaction
(and could set other parameters as well) before `post`ing the data to the PayTrace API.

## Build the Checkout Form

(In `app/views/cart/checkout.html.erb`)

The assumption we made about `params[:sale]` containing the information necessary to
process the sale transaction made the code in `CartController#pay` short and sweet.  Rails
makes it easy to get that structured data in `params` through a [naming convention][param-naming] for
form controls.  By naming a control `sale[credit_card][expiration_month]`, we tell Rails that
we would like `params[:sale]` to contain a Hash, `params[:sale][:credit_card]` to contain
another Hash, and `params[:sale][:credit_card][:expiration_month]` to receive the value of
the control.

One special thing to note: one text input in the form has a `data-name` attribute instead of a
`name` attribute.  That same input also has the string `encrypted:` as part of the last Hash
key and has CSS class `pt-encrypt`.  These, along with the script tag at the bottom of the page,
set the credit card number up to be encrypted *in the browser* in such a way that only
PayTrace can decrypt it.  This takes you (the merchant) out of PCI scope, because you will
not be handling unencrypted credit card numbers at any point in the payment
process.  <sup>[2](#user-content-fn2)</sup>

## Build the Pay Page

(In `app/views/cart/pay.html.erb`)

This page doesn't have to do much. It just shows the results of processing the
transaction through api.paytrace.com. As described in the controller file, the
controller would normally build a *receipt* model object, save it to the
database, link it to other relevant models, and redirect to a page (probably
including the receipt ID in the URL) which would display the receipt to the
user. Saving, displaying, and recalling the particulars of the transaction are
beyond the scope of this demonstration. The transaction will be recorded and
reviewable on the PayTrace [Virtual Terminal][] as with other transactions processed
through PayTrace.

## Serve Your Public Key

The "checkout" form (`app/views/cart/checkout.html.erb`) included in this demonstration
references a public key for our Demo merchant at `/e2ee-public-key.pem` (near the bottom
of the file).  You will need to obtain the PEM file corresponding to your merchant
account (for now, limited to selected PayTrace technology partners) and serve it from your
server.  Put the URL of the PEM file in the `data-paytrace-keyurl` attribute of the
`script` element that loads the client-side-encryption library.  This demonstration puts
the public key for the demo account in `public/e2ee-public-key.pem` so it is available
from the development server.  Again, provide this however you like.

What Data Should I Send to the API?
===================================

PayTrace is working on a full set of documentation for the API.  The interface is still
in flux and the documentation is not yet complete.  If you're asking the question, you've
arrived 5 hours early for the game!

- - - - - - -

### Footnotes

<a name="fn1">\[1\]</a> There are a few supplementary URLs at api.paytrace.com that do not
require authentication via access token, but they are informational in nature and never provide any
confidential information about or access to your PayTrace account.

<a name="fn2">\[2\]</a> The fact that the input has no `name` attribute means that its
value can only be submitted via JavaScript; even if our encryption library fails to
load in the browser, your server will not receive the plain-text credit card number.




[OAuth 2.0]: http://oauth.net/2/
[Vagrant]: https://www.vagrantup.com
[VirtualBox]: https://www.virtualbox.org
[Virtual Terminal]: https://www.paytrace.com
[param-naming]: http://guides.rubyonrails.org/action_controller_overview.html#hash-and-array-parameters
